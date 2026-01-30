import {ModuleRegistryExtend} from "cs2/modding";
import React, {useCallback, useEffect, useMemo, useState} from "react";
import {Icon, PanelFoldout, PanelSection, PanelSectionRow, Tooltip} from "cs2/ui";
import {call, trigger, useValue} from "cs2/api";
import checkSrc from "./icons/check.svg";
import closeSrc from "./icons/close.svg";
import componentStyles from "./NameCandidatesComponent.module.scss";
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

const buttonClass = "button_UgX item-mouse-states_Fmi item-focused_FuT " + componentStyles.container
const okIcon = checkSrc
const removeIcon = closeSrc

const CandidatesFoldout = React.memo((props: {
    name: string | null,
    selectedEntity: Entity,
    candidates: SerializedNameCandidate[],
    initialExpanded?: boolean | undefined,
    onAfterSet?: () => Promise<void>
}) => {
    const {translate} = useLocalization();

    const handleNavigate = useCallback((candidate: SerializedNameCandidate) => {
        navigateToCandidate(StationNaming.toNameCandidate(candidate))
    }, [])

    const handleSet = useCallback(async (candidate: SerializedNameCandidate) => {
        await setSelectedCandidate(toNameCandidate(candidate), props.selectedEntity)
        if (props.onAfterSet) await props.onAfterSet()
    }, [props.onAfterSet, props])

    return (
        <PanelFoldout initialExpanded={props.initialExpanded || false} header={
            <PanelSectionRow left={props.name}/>
        }>
            {(props.candidates || []).map((candidate, idx) =>
                <PanelSectionRow
                    key={(candidate.Name || "") + ":" + idx}
                    left={<>
                        <span>{candidate.Name}</span>
                        <span style={{marginLeft: 8}}>
                            <span className={componentStyles.tag}>
                                {translate(getTranslationKeyOf(
                                    nameSourceToString(combineNameSource(candidate)),
                                    "NameSource"
                                ))}
                            </span>
                        </span>
                    </>}
                    right={
                        <div>
                            <div className={"row_aZw " + componentStyles.container}>
                                <button className={buttonClass}
                                        onClick={() => handleNavigate(candidate)}>
                                    <Icon src="Media/Game/Icons/MapMarker.svg" className={componentStyles.icon}/>
                                </button>
                                <button className={buttonClass}
                                        onClick={() => handleSet(candidate)}>
                                    <img src={okIcon} className={componentStyles.icon} alt="OK"/>
                                </button>
                            </div>
                        </div>
                    }
                />
            )}
        </PanelFoldout>
    )
})


const CandidatesComponent = () => {
    const showCandidates = useValue(isShowCandidates$)
    const selectedEntity = useValue(selectedEntity$)

    const [nameCandidates, setNameCandidates] =
        useState<SerializedNameCandidate[]>([])

    const {translate} = useLocalization()

    const [manualSelected, setManualSelected] = useState<SerializedNameCandidate | null>(null)

    useEffect(() => {
        let mounted = true

        async function fetchCandidates() {
            if (!showCandidates) {
                return
            }

            const candidates = await getNameCandidates(selectedEntity)
            if (!mounted) return
            setNameCandidates(candidates)

            const manual = await getManualSelected(selectedEntity)
            if (!mounted) return
            // normalize empty result
            if (manual && manual.Name) {
                setManualSelected(manual)
            } else {
                setManualSelected(null)
            }
        }

        fetchCandidates();

        return () => {
            mounted = false
        }
    }, [selectedEntity, showCandidates])

    // separate block for current manual selection UI
    const ManualSelectedBlock = useCallback(() => {
        if (!manualSelected) return <></>

        const source = nameSourceToString(combineNameSource(manualSelected))

        return (
            <div>
                {/*TODO: i18n*/}
                <PanelSectionRow left={
                    <span>Current Selected Naming</span>
                }/>
                {/*TODO: if none selected, shows "No naming selected"*/}
                <PanelSectionRow
                    left={<>
                        <span>{manualSelected.Name}</span>
                        <span style={{marginLeft: 8}}>
                        <span
                            className={componentStyles.tag}>{translate(getTranslationKeyOf(source, "NameSource"))}</span>
                    </span>
                    </>}
                    right={
                        <div className={"row_aZw " + componentStyles.container}>
                            <button className={buttonClass}
                                    onClick={() => navigateToCandidate(StationNaming.toNameCandidate(manualSelected))}>
                                <Icon src="Media/Game/Icons/MapMarker.svg" className={componentStyles.icon}/>
                            </button>
                            <button className={buttonClass}
                                    onClick={() => {
                                        trigger(ModName, "removeManualSelected", selectedEntity)
                                        setManualSelected(null)
                                    }}>
                                <img src={removeIcon} className={componentStyles.icon}/>
                            </button>
                        </div>
                    }
                />
            </div>

        )
    }, [manualSelected, selectedEntity, translate])

    const [generalCandidates, spawnableCandidates] = useMemo(() => {
        const general = nameCandidates.filter(
            candidate =>
                combineNameSource(candidate) !== NameSource.SpawnableBuilding
        )

        const spawnable = nameCandidates.filter(
            candidate =>
                combineNameSource(candidate) === NameSource.SpawnableBuilding
        )

        return [general, spawnable]
    }, [nameCandidates])


    if (!showCandidates) return <></>

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

            {/* Spawnable section: only show when there are spawnable candidates */}
            {spawnableCandidates && spawnableCandidates.length > 0 && (
                <>
                    <PanelSectionRow
                        left={translate(
                            getTranslationKeyOf("SpawnableCandidates"),
                            "Spawnable Candidates")
                        }
                        uppercase={true}
                        right={
                            spawnableCandidates.length
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
                </>
            )}
        </PanelSection>
    )
}

export const InfoPanelExtComponent: ModuleRegistryExtend = (components: any): any => {
    components[CandidatesSectionKey] = () =>
        <CandidatesComponent/>

    return components as any
}
