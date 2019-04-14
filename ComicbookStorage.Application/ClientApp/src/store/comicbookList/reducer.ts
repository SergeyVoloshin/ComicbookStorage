import { ComicbookListState, ComicbookListActionTypes, COMICBOOK_LIST_REQUEST, COMICBOOK_LIST_RESPONSE } from './types'

const initialState: ComicbookListState = {
    comicbooks: [],
    isFetching: false
}

const Tiles1 = [
    { id: 1, title: 'test comicbook title 1', coverUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', description: 'Some comicbook description 1' },
    { id: 2, title: 'test comicbook title 1', coverUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', description: 'Some comicbook description 1' },
    { id: 3, title: 'test comicbook title 1', coverUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', description: 'Some comicbook description 1' },
    { id: 4, title: 'test comicbook title 1', coverUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', description: 'Some comicbook description 1' },
    { id: 5, title: 'test comicbook title 1', coverUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1', description: 'Some comicbook description 1' },
];

export function comicbookListReducer(
    state = initialState,
    action: ComicbookListActionTypes
): ComicbookListState {
    switch (action.type) {
        case COMICBOOK_LIST_REQUEST:
            return Object.assign({}, state, {
                isFetching: true,
            });
        case COMICBOOK_LIST_RESPONSE:
            return Object.assign({}, state, {
                isFetching: false,
                comicbooks: action.comicbooks
            });
        default:
            return state;
    }
}