import React, { Component } from 'react';
import { bindActionCreators, Dispatch } from 'redux';
import { connect } from 'react-redux';
import InfiniteScroll from 'react-infinite-scroller';
import { requestComicbookListAsync } from '../store/comicbookList/thunks';
import { ApplicationState } from '../store/configureStore';
import TileGrid from '../components/TileGrid';
import Loading from "../components/Loading";

interface ComicbookListSettings {
    pageSize: number
}

type ComicbookListProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps> &
    ComicbookListSettings;

export class ComicbookList extends Component<ComicbookListProps> {

    constructor(props: ComicbookListProps) {
        super(props);
        this.loadMoreComicbooks = this.loadMoreComicbooks.bind(this);
    }

    loadMoreComicbooks(pageNumber: number) {
        let pageSize: number = 50;
        if (typeof this.props.pageSize !== 'undefined') {
            pageSize = this.props.pageSize;
        }
        this.props.requestComicbookList(pageNumber, pageSize);
    }

    render() {
        let tiles = this.props.comicbookListState.comicbooks.map((comicbook) => ({
            id: comicbook.id,
            url: '#',
            imageUrl: comicbook.coverUrl,
            title: comicbook.title,
            description: comicbook.description,
        }));

        return (
            <div>
                <InfiniteScroll
                    pageStart={this.props.comicbookListState.currentPage}
                    loadMore={this.loadMoreComicbooks}
                    hasMore={this.props.comicbookListState.hasMore}
                    loader={<Loading key={0} />}
                    useWindow={true}>
                    <TileGrid tiles={tiles} />
                </InfiniteScroll>
            </div>);
    }
}

const mapStateToProps = (state: ApplicationState) => {
    return { comicbookListState: state.comicbookList, }
}

const mapDispatchToProps = (dispatch: Dispatch) => bindActionCreators(
    {
        requestComicbookList: requestComicbookListAsync,
    },
    dispatch
);

export default connect(mapStateToProps, mapDispatchToProps)(ComicbookList)
