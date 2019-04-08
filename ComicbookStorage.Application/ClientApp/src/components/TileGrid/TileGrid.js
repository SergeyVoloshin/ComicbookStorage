import React from 'react'
import Tile from './Tile'
import PropTypes from 'prop-types'


class TileGrid extends React.Component {
    constructor(props) {
        super(props);
        this.state = { rowLength: 4 };
    }

    render() {
        let renderRows = [];
        let i = 0;
        while (i < this.props.tiles.length) {
            let rowEnd = Math.min(i + this.state.rowLength, this.props.tiles.length);
            let renderTiles = [];
            for (; i < rowEnd; i++) {
                let tile = this.props.tiles[i];
                renderTiles.push(
                    <div key={tile.id} className="col-md-3">
                        <Tile tileUrl={tile.url} imageUrl={tile.imageUrl} title={tile.title} description={tile.description
                        } />
                    </div>);
            }
            renderRows.push(<div key={rowEnd} className="row pt-3">{renderTiles}</div>);
        }

        return (
            <div className="container">
                {renderRows}
            </div>
        );
    }
}


TileGrid.propTypes = {
    tiles: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number.isRequired,
            url: PropTypes.string.isRequired,
            imageUrl: PropTypes.string.isRequired,
            title: PropTypes.string.isRequired,
            description: PropTypes.string
        }).isRequired
    ).isRequired
}

export default TileGrid