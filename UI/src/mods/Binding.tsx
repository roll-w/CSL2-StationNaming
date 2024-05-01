import {selectedInfo} from "cs2/bindings";
import {CandidatesSectionKey} from "./NameCandidatesComponent";
import {bindValue, useValue} from "cs2/api";
import {StationNaming} from "./base";
import ModName = StationNaming.ModName;
import {ModuleRegistryAppend} from "cs2/modding";

let currentEntity: any = null;
const selectedEntity$ = selectedInfo.selectedEntity$;
const middleSections$ = selectedInfo.middleSections$;

const isShowCandidates$ = bindValue<boolean>(
    ModName, "IsShowCandidates", false);

const isShowCandidates = () => {
    return useValue(isShowCandidates$);
}

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
