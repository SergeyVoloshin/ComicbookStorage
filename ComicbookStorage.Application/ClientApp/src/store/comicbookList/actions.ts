﻿import { ComicbookListItem, COMICBOOK_LIST_RESPONSE, ComicbookListReceivedAction, ComicbookListItemDto, ComicbookListDto } from './types';

export function receiveComicbookList(json: ComicbookListDto): ComicbookListReceivedAction {
    let transformedList: ComicbookListItem[] = json.comicbooks.map((dto: ComicbookListItemDto): ComicbookListItem => ({
        id: dto.id,
        title: dto.name,
        coverUrl: dto.coverUrl,
        description: dto.description,
    }));

    return {
        type: COMICBOOK_LIST_RESPONSE,
        comicbooks: transformedList,
        hasMore: json.hasMore,
    }
}
