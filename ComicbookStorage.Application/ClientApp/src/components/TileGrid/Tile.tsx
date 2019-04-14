import React from 'react'

export interface TileProps {
    url: string,
    imageUrl: string,
    title: string,
    description: string,
}

const Tile = ({ url, imageUrl, title, description }: TileProps) => (
    <div className="card">
        <img className="card-img-top" src={imageUrl} alt={title} />
        <div className="card-body">
            <h5 className="card-title"><a href={url}>{title}</a></h5>
            <p className="card-text">{description}</p>
        </div>
    </div>
);

export default Tile