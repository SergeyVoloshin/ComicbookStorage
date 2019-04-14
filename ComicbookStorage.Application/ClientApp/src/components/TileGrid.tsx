import React, { Component } from 'react';
import Tile, { TileProps } from './Tile';

interface TileGridProps {
    tiles: GridTile[]
}

interface GridTile extends TileProps {
    id: number
}

interface TileGridState {
    rowLength: number
}

class TileGrid extends Component<TileGridProps, TileGridState> {
    constructor(props: TileGridProps) {
        super(props);

        this.state = {
            rowLength: 4
        };
    }

    render() {
        let renderRows: JSX.Element[] = [];
        let i = 0;
        while (i < this.props.tiles.length) {
            let rowEnd = Math.min(i + this.state.rowLength, this.props.tiles.length);
            let renderTiles: JSX.Element[] = [];
            for (; i < rowEnd; i++) {
                let tile = this.props.tiles[i];
                renderTiles.push(
                    <div key={tile.id} className="col-md-3">
                        <Tile url={tile.url} imageUrl={tile.imageUrl} title={tile.title} description={tile.description} />
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

export default TileGrid