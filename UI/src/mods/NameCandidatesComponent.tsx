import {ModuleRegistryExtend} from "cs2/modding";
import React, {useEffect, useState} from "react";
import {Icon, PanelFoldout, PanelSection, PanelSectionRow, Tooltip} from "cs2/ui";
import {call, trigger, useValue} from "cs2/api";
import {Entity} from "cs2/bindings";
import {StationNaming} from "./base";
import NameCandidate = StationNaming.NameCandidate;
import SerializedNameCandidate = StationNaming.SerializedNameCandidate;
import ModName = StationNaming.ModName;
import nameSourceToString = StationNaming.nameSourceToString;
import toNameCandidate = StationNaming.toNameCandidate;
import {useLocalization} from "cs2/l10n";
import getTranslationKeyOf = StationNaming.getTranslationKeyOf;
import combineNameSource = StationNaming.combineNameSource;
import NameSource = StationNaming.NameSource;
import isShowCandidates$ = StationNaming.isShowCandidates$;
import selectedEntity$ = StationNaming.selectedEntity$;

export const CandidatesSectionKey = "StationNaming.NameCandidates"

const setSelectedCandidate = (candidate: NameCandidate, selectedEntity: Entity) => {
    call(ModName, "setCandidateFor", selectedEntity, candidate).then(r => {
    })
}

const navigateToCandidate = (candidate: NameCandidate) => {
    trigger(ModName, "navigateToCandidate", candidate)
}

const getNameCandidates = async (selectedEntity: Entity): Promise<any> => {
    return await call(
        ModName,
        "getCandidates",
        selectedEntity) as unknown as Promise<SerializedNameCandidate[]>
}

const CandidatesFoldout = (props: {
    name: string | null,
    selectedEntity: Entity,
    candidates: SerializedNameCandidate[],
    initialExpanded?: boolean | undefined
}) => {
    const {translate} = useLocalization();

    const buttonClass = "button_UgX item-mouse-states_Fmi item-focused_FuT"

    return (
        <PanelFoldout initialExpanded={props.initialExpanded || false} header={
            <PanelSectionRow left={props.name}/>
        }>
            {(props.candidates || []).map(candidate =>
                <PanelSectionRow
                    left={candidate.Name + " [" +
                        translate(getTranslationKeyOf(
                            nameSourceToString(combineNameSource(candidate)),
                            "NameSource"
                        ))
                        + "]"}
                    right={
                        <div className={"row_aZw"}>
                            <button className={buttonClass}
                                    onClick={() => navigateToCandidate(StationNaming.toNameCandidate(candidate))}>
                                <Icon src="Media/Game/Icons/MapMarker.svg"/>
                            </button>
                            <button className={buttonClass}
                                    onClick={() => setSelectedCandidate(toNameCandidate(candidate), props.selectedEntity)}>
                                ✓
                            </button>
                        </div>
                    }
                />
            )}
        </PanelFoldout>
    )
}


const CandidatesComponent = () => {
    const showCandidates = useValue(isShowCandidates$)
    const selectedEntity = useValue(selectedEntity$)

    const [nameCandidates, setNameCandidates] =
        useState<SerializedNameCandidate[]>([])

    const [generalCandidates, setGeneralCandidates] =
        useState<SerializedNameCandidate[]>([])

    const [spawnableCandidates, setSpawnableCandidates] =
        useState<SerializedNameCandidate[]>([])

    useEffect(() => {
        async function fetchCandidates() {
            if (!showCandidates) {
                return
            }

            const candidates = await getNameCandidates(selectedEntity)
            setNameCandidates(candidates)
        }

        fetchCandidates();
    }, [selectedEntity])

    useEffect(() => {
        const generalCandidates = nameCandidates.filter(
            candidate =>
                combineNameSource(candidate) !== NameSource.SpawnableBuilding
        )

        const spawnableCandidates = nameCandidates.filter(
            candidate =>
                combineNameSource(candidate) === NameSource.SpawnableBuilding
        )

        setGeneralCandidates(generalCandidates)
        setSpawnableCandidates(spawnableCandidates)
    }, [nameCandidates])

    const {translate} = useLocalization()

    if (!showCandidates) {
        return <></>
    }

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
                    (generalCandidates || []).length
                }
            />

            <CandidatesFoldout
                name={translate(getTranslationKeyOf("Candidates"))}
                selectedEntity={selectedEntity}
                candidates={generalCandidates}
            />

            <PanelSectionRow
                left={translate(
                    getTranslationKeyOf("SpawnableCandidates"),
                    "Spawnable Candidates")
                }
                uppercase={true}
                right={
                    (spawnableCandidates || []).length
                }
            />

            <CandidatesFoldout
                name={translate(getTranslationKeyOf("Candidates"))}
                selectedEntity={selectedEntity}
                candidates={spawnableCandidates}/>
        </PanelSection>
    )
}

export const InfoPanelExtComponent: ModuleRegistryExtend = (components: any): any => {
    components[CandidatesSectionKey] = () =>
        <CandidatesComponent/>

    return components as any
}
