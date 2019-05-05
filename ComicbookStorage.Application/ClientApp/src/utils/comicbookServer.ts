import fetch from 'cross-fetch';
import { store } from '..';
import { setProgressBar } from '../store/commonUi/actions';

class ComicbookServer {
    get<T>(url: string, showProgressbar: boolean = true): Promise<T> {
        this.checkProgressStart(showProgressbar);

        return fetch(url)
            .then(
            response => {
                    this.checkProgressStop(showProgressbar);
                    return response.json();
                },
                error => console.log('An error occurred.', error)
            );
    }

    checkProgressStart(showProgressbar: boolean) {
        if (showProgressbar) {
            store.dispatch(setProgressBar(true));
        }
    }

    checkProgressStop(showProgressbar: boolean) {
        if (showProgressbar) {
            store.dispatch(setProgressBar(false));
        }
    }
}

const comicbookServer = new ComicbookServer();

export default comicbookServer