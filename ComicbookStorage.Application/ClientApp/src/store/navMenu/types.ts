export interface NavMenuState {
    isOpen: boolean,
}

export const TOGGLE_MENU = 'TOGGLE_MENU';

export interface ToggleAction {
    type: typeof TOGGLE_MENU,
}

export type NavMenuActionTypes = ToggleAction