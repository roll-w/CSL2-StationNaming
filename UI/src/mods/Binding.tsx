import {Entity, selectedInfo} from "cs2/bindings";
import {CandidatesSectionKey} from "./NameCandidatesComponent";

let currentEntity: any = null;
const selectedEntity$ = selectedInfo.selectedEntity$;
const middleSections$ = selectedInfo.middleSections$;

export const InfoPanelBinding = () => {
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
            // if (value.find(item => item?.__Type == SectionType.Lines)) {
            // SectionType.Lines is undefined here so we use the string value instead
            if (value.find(item => item?.__Type === "Game.UI.InGame.LinesSection" as any)) {
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
