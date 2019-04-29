import { ComicbookListState, ComicbookListActionTypes, COMICBOOK_LIST_RESPONSE } from './types';

const initialState: ComicbookListState = {
    comicbooks: [],
    hasMore: true,
    currentPage: 0,
}

export function comicbookListReducer(
    state = initialState,
    action: ComicbookListActionTypes
): ComicbookListState {
    switch (action.type) {
        case COMICBOOK_LIST_RESPONSE:
            return {
                ...state,
                comicbooks: [...state.comicbooks, ...action.comicbooks],
                hasMore: action.hasMore,
                currentPage: state.currentPage + 1,
            }
        default:
            return state;
    }
}