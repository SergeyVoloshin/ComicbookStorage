import { ComicbookListState, ComicbookListActionTypes, COMICBOOK_LIST_REQUEST, COMICBOOK_LIST_RESPONSE } from './types';

const initialState: ComicbookListState = {
    comicbooks: [],
}

export function comicbookListReducer(
    state = initialState,
    action: ComicbookListActionTypes
): ComicbookListState {
    switch (action.type) {
        case COMICBOOK_LIST_RESPONSE:
            return Object.assign({}, state, {
                comicbooks: action.comicbooks
            });
        case COMICBOOK_LIST_REQUEST:
        default:
            return state;
    }
}