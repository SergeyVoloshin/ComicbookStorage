import React, { Component } from 'react';
import { bindActionCreators, Dispatch } from 'redux'
import { connect } from 'react-redux'
import { getComicbooks } from '../store/comicbookList/actions';
import { ApplicationState } from '../store/configureStore';
import TileGrid from '../components/TileGrid';
import { ComicbookListActionTypes } from '../store/comicbookList/types'

type ComicbookListProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps>;

export class ComicbookList extends Component<ComicbookListProps> {

    constructor(props: ComicbookListProps) {
        super(props);
    }

    componentDidMount() {
        this.props.requestComicbookList();
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
                <TileGrid tiles={tiles} />
            </div>);
    }
}

const mapStateToProps = (state: ApplicationState) => {
    return { comicbookListState: state.comicbookList }
}

const mapDispatchToProps = (dispatch: Dispatch) => bindActionCreators(
    {
        requestComicbookList: getComicbooks,
    },
    dispatch
);

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ComicbookList)
