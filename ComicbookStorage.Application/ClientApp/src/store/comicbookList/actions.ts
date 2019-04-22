import { ComicbookListItem, COMICBOOK_LIST_RESPONSE, ComicbookListActionTypes, ComicbookListItemDto, ComicbookListDto } from './types';

export function receiveComicbookList(json: ComicbookListDto): ComicbookListActionTypes {
    let transformedList: ComicbookListItem[] = json.comicbooks.map((serverItem: ComicbookListItemDto): ComicbookListItem => ({
        id: serverItem.id,
        title: serverItem.name,
        coverUrl: serverItem.coverUrl,
        description: serverItem.description,
    }));

    return {
        type: COMICBOOK_LIST_RESPONSE,
        comicbooks: transformedList,
        hasMore: json.hasMore,
    }
}
