import React, { Component } from 'react';
import { bindActionCreators, Dispatch } from 'redux';
import { connect } from 'react-redux';
import InfiniteScroll from 'react-infinite-scroller';
import { requestComicbookListAsync } from '../store/comicbookList/thunks';
import { ApplicationState } from '../store/configureStore';
import TileGrid from '../components/TileGrid';

type ComicbookListProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps>;

export class ComicbookList extends Component<ComicbookListProps> {

    constructor(props: ComicbookListProps) {
        super(props);
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
                    pageStart={0}
                    loadMore={this.props.requestComicbookList}
                    hasMore={this.props.comicbookListState.hasMore}
                    loader={<div key={0}>Loading ...</div>}
                    useWindow={true}
                >
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

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ComicbookList)
