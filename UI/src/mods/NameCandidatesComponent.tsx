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
import toNameCandidate = StationNaming.toNameCandidate;
import {useLocalization} from "cs2/l10n";
import getTranslationKeyOf = StationNaming.getTranslationKeyOf;

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

    const {translate} = useLocalization();

    return (
        <PanelSection tooltip={
            <Tooltip tooltip={<div></div>}>
                <div>Here is all the name candidates, you can choose them to replace the default name.</div>
            </Tooltip>
        }>
            <PanelSectionRow
                left={translate(
                    getTranslationKeyOf("NameCandidates"),
                    "Name Candidates")
                }
                uppercase={true}
                right={
                    (nameCandidates || []).length
                }
            />
            <PanelFoldout initialExpanded={true} header={
                <PanelSectionRow left={translate(
                    getTranslationKeyOf("Candidates"))}/>
            }>
                {(nameCandidates || []).map(candidate =>
                    <PanelSectionRow
                        left={candidate.Name + " [" +
                            translate(
                                getTranslationKeyOf(nameSourceToString(candidate.Source.value__), "NameSource"),
                                nameSourceToString(candidate.Source.value__)
                            ) + "]"}
                        link={
                            <div onClick={() => {
                                setSelectedCandidate(toNameCandidate(candidate));
                            }}>
                                Adopt
                            </div>
                        }
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
