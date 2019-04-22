
export interface ComicbookListItemDto {
    id: number,
    name: string,
    description: string,
    coverUrl: string,
    url: string,
}

export interface ComicbookListDto {
    comicbooks: ComicbookListItemDto[],
    hasMore: boolean,
}

export interface ComicbookListItem {
    id: number,
    title: string,
    coverUrl: string,
    description: string,
}

export interface ComicbookListState {
    comicbooks: ComicbookListItem[],
    hasMore: boolean,
}

export const COMICBOOK_LIST_RESPONSE = 'COMICBOOK_LIST_RESPONSE';


interface ReceiveComicbookListAction {
    type: typeof COMICBOOK_LIST_RESPONSE,
    comicbooks: ComicbookListItem[],
    hasMore: boolean,
}

export type ComicbookListActionTypes = ReceiveComicbookListAction