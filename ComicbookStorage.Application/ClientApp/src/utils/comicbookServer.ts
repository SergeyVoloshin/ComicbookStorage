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
                }
            );
    }

    post<T>(url: string, data: T, showProgressbar: boolean = true): Promise<Response> {
        this.checkProgressStart(showProgressbar);

        return fetch(url,
            {
                method: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data),
            })
            .then(
                response => {
                    this.checkProgressStop(showProgressbar);
                    return response;
                }
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