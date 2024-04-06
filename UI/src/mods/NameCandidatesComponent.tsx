import {ModuleRegistryExtend} from "cs2/modding";

import React, {useEffect, useState} from "react";
import {PanelFoldout, PanelSection, PanelSectionRow, Tooltip} from "cs2/ui";
import {call, trigger} from "cs2/api";
import {selectedInfo, Entity} from "cs2/bindings";
import {StationNaming} from "./base";
import NameCandidate = StationNaming.NameCandidate;
import SerializedNameCandidate = StationNaming.SerializedNameCandidate;
import ModName = StationNaming.ModName;
import nameSourceToString = StationNaming.nameSourceToString;

export const CandidatesSectionKey = "StationNaming.NameCandidates";

let selectedEntity: Entity;

const selectedEntityChanged = (newEntity: Entity) => {
    selectedEntity = newEntity;
    trigger(ModName, "SetSelectedEntity", newEntity);
}

const setSelectedCandidate = (candidate: NameCandidate) => {
    trigger(ModName, "SetCandidateFor", selectedEntity, candidate);
}

const getNameCandidates = async (): Promise<SerializedNameCandidate[]> => {
    return await call(
        ModName,
        "GetCandidates",
        selectedEntity) as unknown as Promise<SerializedNameCandidate[]>
}


const CandidatesComponent = () => {
    const [nameCandidates, setNameCandidates] =
        useState<SerializedNameCandidate[]>([])

    useEffect(() => {
        async function fetchCandidates() {
            const candidates = await getNameCandidates();
            setNameCandidates(candidates);
        }

        fetchCandidates();
    }, [selectedEntity])

    return (
        <PanelSection tooltip={
            <Tooltip tooltip={<div></div>}>
                <div>Here is all the name candidates, you can choose them to replace the default name.</div>
            </Tooltip>
        }>
            <PanelSectionRow
                left={"Name Candidates"}
                uppercase={true}
                right={
                    (nameCandidates || []).length
                }
            />
            <PanelFoldout initialExpanded={true} header={
                <PanelSectionRow left={"Candidates"}/>
            }>
                <PanelSectionRow left={"Name"} right={"Source"}/>
                {(nameCandidates || []).map(candidate =>
                    <PanelSectionRow
                        left={candidate.Name}
                        right={nameSourceToString(candidate.Source.value__)}
                    />
                )}
            </PanelFoldout>
        </PanelSection>
    )
}

export const InfoPanelExtComponent: ModuleRegistryExtend = (components: any): any => {
    selectedInfo.selectedEntity$.subscribe(selectedEntityChanged);

    components[CandidatesSectionKey] = () =>
        <CandidatesComponent/>

    return components as any
}
