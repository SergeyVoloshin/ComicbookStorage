import { NavMenuState, NavMenuActionTypes, TOGGLE_MENU } from './types';

const initialState: NavMenuState = {
    isOpen: false,
}

export function navMenuReducer(
    state = initialState,
    action: NavMenuActionTypes
): NavMenuState {
    switch (action.type) {
        case TOGGLE_MENU:
        return {
            ...state,
            isOpen: !state.isOpen,
        }
    default:
        return state;
    }
}