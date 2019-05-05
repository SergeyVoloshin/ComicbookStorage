import { Dispatch } from 'redux';
import { ComicbookListReceivedAction, ComicbookListDto } from './types';
import { receiveComicbookList } from './actions';
import comicbookServer from '../../utils/comicbookServer';

export const requestComicbookListAsync = (pageNumber: number, pageSize: number) => (dispatch: Dispatch): Promise<ComicbookListReceivedAction> => {
    return comicbookServer.get<ComicbookListDto>('/comicbook/get-page/' + pageNumber + '/' + pageSize, false)
        .then(json => dispatch(receiveComicbookList(json)));
}