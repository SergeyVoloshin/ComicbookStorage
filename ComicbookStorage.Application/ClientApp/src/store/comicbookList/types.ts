export interface ComicbookListItem
{
    id: number,
    title: string,
    coverUrl: string,
    description: string,
}

export interface ComicbookListState {
    comicbooks: ComicbookListItem[]
    isFetching: boolean
}

export const COMICBOOK_LIST_REQUEST = 'COMICBOOK_LIST_REQUEST';
export const COMICBOOK_LIST_RESPONSE = 'COMICBOOK_LIST_RESPONSE';

interface RequestComicbookListAction {
    type: typeof COMICBOOK_LIST_REQUEST
}

interface ReceiveComicbookListAction {
    type: typeof COMICBOOK_LIST_RESPONSE
    comicbooks: ComicbookListItem[]
}

export type ComicbookListActionTypes = RequestComicbookListAction | ReceiveComicbookListAction