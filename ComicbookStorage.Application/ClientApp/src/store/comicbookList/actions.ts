import fetch from 'cross-fetch'
import { Dispatch  } from 'redux'
import { ComicbookListItem, COMICBOOK_LIST_REQUEST, COMICBOOK_LIST_RESPONSE, ComicbookListActionTypes } from './types'

export function requestComicbookList(): ComicbookListActionTypes {
    return {
        type: COMICBOOK_LIST_REQUEST,
    }
}

export function receiveComicbookList(json: any): ComicbookListActionTypes {
    let transformedList: ComicbookListItem[] = json.map((serverItem: any): ComicbookListItem => ({
        id: serverItem.id,
        title: serverItem.name,
        coverUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1',
        description: serverItem.name,
    }))

    console.log(transformedList);
    return {
        type: COMICBOOK_LIST_RESPONSE,
        comicbooks: transformedList
    }
}

export const getComicbooks = () => async (dispatch: Dispatch<ComicbookListActionTypes>): Promise<ComicbookListActionTypes> => {
    dispatch(requestComicbookList())
    return fetch('/api/comicbook')
        .then(
            response => response.json(),
            error => console.log('An error occurred.', error)
        )
        .then(json =>
            dispatch(receiveComicbookList(json))
        );
}
