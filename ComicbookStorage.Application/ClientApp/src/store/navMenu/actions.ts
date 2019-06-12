import { ToggleAction, TOGGLE_MENU } from './types';

export function toggleMenu(): ToggleAction {
    return {
        type: TOGGLE_MENU,
    }
}