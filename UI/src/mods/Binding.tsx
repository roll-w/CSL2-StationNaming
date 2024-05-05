import {selectedInfo} from "cs2/bindings";
import {CandidatesSectionKey} from "./NameCandidatesComponent";
import {StationNaming} from "./base";
import {ModuleRegistryAppend} from "cs2/modding";
import isShowCandidates = StationNaming.isShowCandidates;

let currentEntityIndex: number = 0;
const selectedEntity$ = selectedInfo.selectedEntity$;
const middleSections$ = selectedInfo.middleSections$;

export const InfoPanelBinding: ModuleRegistryAppend = () => {
    const showCandidates = isShowCandidates();

    selectedEntity$.subscribe((entity) => {
        if (!entity.index) {
            currentEntityIndex = 0
            return entity
        }
        if (currentEntityIndex != entity.index) {
            currentEntityIndex = entity.index
        }
        return entity
    })

    middleSections$.subscribe(value => {
        if (currentEntityIndex && value.every(item => item?.__Type !== CandidatesSectionKey as any)) {
            if (showCandidates) {
                value.unshift({
                    __Type: CandidatesSectionKey,
                    group: "DescriptionSection"
                } as any)

                const desc = value[1]
                value[1] = value[0]
                value[0] = desc
            }
        }
    })
    return <></>
}
