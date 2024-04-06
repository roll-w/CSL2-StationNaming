import {ModRegistrar} from "cs2/modding";
import {InfoPanelExtComponent} from "./mods/NameCandidatesComponent";
import {InfoPanelBinding} from "./mods/Binding";

const register: ModRegistrar = (moduleRegistry) => {
    console.log("Inject StationNaming ui.")

    moduleRegistry.append("Game", InfoPanelBinding)
    moduleRegistry.extend(
        "game-ui/game/components/selected-info-panel/selected-info-sections/selected-info-sections.tsx",
        'selectedInfoSectionComponents',
        InfoPanelExtComponent
    )
}

export default register;