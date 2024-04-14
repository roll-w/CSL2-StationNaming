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
import combineNameSource = StationNaming.combineNameSource;

export const CandidatesSectionKey = "StationNaming.NameCandidates";

let selectedEntity: Entity;

const selectedEntityChanged = (newEntity: Entity) => {
    selectedEntity = newEntity;
    trigger(ModName, "SetSelectedEntity", newEntity);
}

const setSelectedCandidate = (candidate: NameCandidate) => {
    call(ModName, "SetCandidateFor", selectedEntity, candidate).then(r => {
    });
}

const getNameCandidates = async (): Promise<any> => {
    return await call(
        ModName,
        "GetCandidates",
        selectedEntity) as unknown as Promise<SerializedNameCandidate[]>
}

const CandidatesFoldout = (props: {
    name: string | null,
    candidates: SerializedNameCandidate[]
}) => {
    const {translate} = useLocalization();

    return (
        <PanelFoldout initialExpanded={false} header={
            <PanelSectionRow left={props.name}/>
        }>
            {(props.candidates || []).map(candidate =>
                <PanelSectionRow
                    left={candidate.Name + " [" +
                        translate(getTranslationKeyOf(
                            nameSourceToString(combineNameSource(candidate.Refers)),
                            "NameSource"
                        ))
                        +"]"}
                    link={
                        <div onClick={() => {
                            setSelectedCandidate(toNameCandidate(candidate))
                        }}>
                            ✓
                        </div>
                    }
                />
            )}
        </PanelFoldout>
    )
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
                <div>{
                    translate(getTranslationKeyOf("NameCandidates[Tooltip]"))
                }</div>
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

            <CandidatesFoldout
                name={translate(getTranslationKeyOf("Candidates"))}
                candidates={nameCandidates}
            />
        </PanelSection>
    )
}

export const InfoPanelExtComponent: ModuleRegistryExtend = (components: any): any => {
    selectedInfo.selectedEntity$.subscribe(selectedEntityChanged);

    components[CandidatesSectionKey] = () =>
        <CandidatesComponent/>

    return components as any
}
