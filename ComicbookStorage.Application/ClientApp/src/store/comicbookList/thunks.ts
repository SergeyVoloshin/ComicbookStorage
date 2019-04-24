import { Dispatch } from 'redux';
import { ComicbookListActionTypes, ComicbookListDto } from './types';
import { receiveComicbookList } from './actions';
import comicbookServer from '../../utils/comicbookServer';

export const requestComicbookListAsync = (pageNumber: number, pageSize: number) => (dispatch: Dispatch): Promise<ComicbookListActionTypes> => {
    return comicbookServer.get<ComicbookListDto>('/comicbook/' + pageNumber + '/' + pageSize, false)
        .then(json => dispatch(receiveComicbookList(json)));
}