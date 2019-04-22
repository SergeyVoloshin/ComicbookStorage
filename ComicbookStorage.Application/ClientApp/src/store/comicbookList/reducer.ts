import { ComicbookListState, ComicbookListActionTypes, COMICBOOK_LIST_RESPONSE } from './types';

const initialState: ComicbookListState = {
    comicbooks: [],
    hasMore: true,
}

export function comicbookListReducer(
    state = initialState,
    action: ComicbookListActionTypes
): ComicbookListState {
    switch (action.type) {
        case COMICBOOK_LIST_RESPONSE:
            return Object.assign({}, state, {
                comicbooks: [...state.comicbooks, ...action.comicbooks],
                hasMore: action.hasMore,
            });
        default:
            return state;
    }
}