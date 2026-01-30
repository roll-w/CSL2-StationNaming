import {ModuleRegistryExtend} from "cs2/modding";
import React, {useEffect, useState} from "react";
import {Icon, PanelFoldout, PanelSection, PanelSectionRow, Tooltip} from "cs2/ui";
import {call, trigger, useValue} from "cs2/api";
import checkSrc from "./icons/check.svg";
import closeSrc from "./icons/close.svg";
import iconStyles from "./icon.module.scss";
import {Entity} from "cs2/bindings";
import {StationNaming} from "./base";
import {useLocalization} from "cs2/l10n";
import NameCandidate = StationNaming.NameCandidate;
import SerializedNameCandidate = StationNaming.SerializedNameCandidate;
import ModName = StationNaming.ModName;
import nameSourceToString = StationNaming.nameSourceToString;
import toNameCandidate = StationNaming.toNameCandidate;
import getTranslationKeyOf = StationNaming.getTranslationKeyOf;
import combineNameSource = StationNaming.combineNameSource;
import NameSource = StationNaming.NameSource;
import isShowCandidates$ = StationNaming.isShowCandidates$;
import selectedEntity$ = StationNaming.selectedEntity$;

export const CandidatesSectionKey = "StationNaming.NameCandidates"

const setSelectedCandidate = (candidate: NameCandidate, selectedEntity: Entity) => {
    // return the promise so callers can await and then refresh UI
    return call(ModName, "setCandidateFor", selectedEntity, candidate) as Promise<any>
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

const getManualSelected = async (selectedEntity: Entity): Promise<any> => {
    return await call(
        ModName,
        "getManualSelected",
        selectedEntity) as unknown as Promise<SerializedNameCandidate>
}

const buttonClass = "button_UgX item-mouse-states_Fmi item-focused_FuT"
const okIcon = checkSrc
const removeIcon = closeSrc

const CandidatesFoldout = (props: {
    name: string | null,
    selectedEntity: Entity,
    candidates: SerializedNameCandidate[],
    initialExpanded?: boolean | undefined,
    onAfterSet?: () => Promise<void>
}) => {
    const {translate} = useLocalization();

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
                        <div>
                            <div className={"row_aZw"}>
                                <button className={buttonClass}
                                        onClick={() => navigateToCandidate(StationNaming.toNameCandidate(candidate))}>
                                    <Icon src="Media/Game/Icons/MapMarker.svg"/>
                                </button>
                                <button className={buttonClass}
                                        onClick={async () => {
                                            await setSelectedCandidate(toNameCandidate(candidate), props.selectedEntity)
                                            if (props.onAfterSet) await props.onAfterSet()
                                        }}>
                                    <img src={okIcon} className={iconStyles.icon} alt="OK"/>
                                </button>
                            </div>
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

    const [manualSelected, setManualSelected] = useState<SerializedNameCandidate | null>(null)

    useEffect(() => {
        async function fetchCandidates() {
            if (!showCandidates) {
                return
            }

            const candidates = await getNameCandidates(selectedEntity)
            setNameCandidates(candidates)
            const manual = await getManualSelected(selectedEntity)
            // normalize empty result
            if (manual && manual.Name) {
                setManualSelected(manual)
            } else {
                setManualSelected(null)
            }
        }

        fetchCandidates();
    }, [selectedEntity])

    // separate block for current manual selection UI
    const ManualSelectedBlock = () => {
        if (!manualSelected) return <></>

        const source = nameSourceToString(combineNameSource(manualSelected))

        return (
            <PanelSectionRow
                left={manualSelected.Name + " [" + translate(getTranslationKeyOf(source, "NameSource")) + "]"}
                right={
                    <div className={"row_aZw"}>
                        <button className={buttonClass}
                                onClick={() => navigateToCandidate(StationNaming.toNameCandidate(manualSelected))}>
                            <Icon src={"Media/Game/Icons/MapMarker.svg"}/>
                        </button>
                        <button className={buttonClass}
                                onClick={async () => {
                                    // use call that removes and returns current manual to avoid race
                                    const manual = await call(ModName, "removeManualSelectedAndGet", selectedEntity) as unknown as SerializedNameCandidate
                                    if (manual && manual.Name) setManualSelected(manual)
                                    else setManualSelected(null)
                                }}>
                            <img src={removeIcon} className={iconStyles.icon}/>
                        </button>
                    </div>
                }
            />
        )
    }

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
            <ManualSelectedBlock/>
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

            {/* show current manual selection separately */}

            <CandidatesFoldout
                name={translate(getTranslationKeyOf("Candidates"))}
                selectedEntity={selectedEntity}
                candidates={generalCandidates}
                onAfterSet={async () => {
                    const manual = await getManualSelected(selectedEntity)
                    if (manual && manual.Name) setManualSelected(manual)
                    else setManualSelected(null)
                }}
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
                candidates={spawnableCandidates}
                onAfterSet={async () => {
                    const manual = await getManualSelected(selectedEntity)
                    if (manual && manual.Name) setManualSelected(manual)
                    else setManualSelected(null)
                }}
            />
        </PanelSection>
    )
}

export const InfoPanelExtComponent: ModuleRegistryExtend = (components: any): any => {
    components[CandidatesSectionKey] = () =>
        <CandidatesComponent/>

    return components as any
}
