import {selectedInfo} from "cs2/bindings";
import {CandidatesSectionKey} from "./NameCandidatesComponent";
import {StationNaming} from "./base";
import {ModuleRegistryAppend} from "cs2/modding";
import isShowCandidates = StationNaming.isShowCandidates;

let currentEntity: any = null;
const selectedEntity$ = selectedInfo.selectedEntity$;
const middleSections$ = selectedInfo.middleSections$;

export const InfoPanelBinding: ModuleRegistryAppend = () => {
    const showCandidates = isShowCandidates();

    selectedEntity$.subscribe((entity) => {
        if (!entity.index) {
            currentEntity = null;
            return entity
        }
        if (currentEntity != entity.index) {
            currentEntity = entity.index
        }
        return entity;
    })

    middleSections$.subscribe(value => {
        if (currentEntity && value.every(item => item?.__Type !== CandidatesSectionKey as any)) {
            if (showCandidates) {
                value.unshift({
                    __Type: CandidatesSectionKey,
                    group: "DescriptionSection"
                } as any);

                const desc = value[1]
                value[1] = value[0]
                value[0] = desc
            }
        }
    })
    return <></>
}
