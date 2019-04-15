import { ComicbookListItem, COMICBOOK_LIST_REQUEST, COMICBOOK_LIST_RESPONSE, ComicbookListActionTypes, ComicbookListItemDto } from './types';


export function requestComicbookList(): ComicbookListActionTypes {
    return {
        type: COMICBOOK_LIST_REQUEST,
    }
}

export function receiveComicbookList(json: ComicbookListItemDto[]): ComicbookListActionTypes {
    let transformedList: ComicbookListItem[] = json.map((serverItem: ComicbookListItemDto): ComicbookListItem => ({
        id: serverItem.id,
        title: serverItem.name,
        coverUrl: 'https://pp.userapi.com/c636128/v636128479/8c8f/pO9xQ4t8HCA.jpg?ava=1',
        description: serverItem.name,
    }));

    return {
        type: COMICBOOK_LIST_RESPONSE,
        comicbooks: transformedList
    }
}
