import React from 'react'
import PropTypes from 'prop-types'

const Tile = ({ tileUrl, imageUrl, title, description }) => (
    <div className="card">
        <img className="card-img-top" src={imageUrl} alt={title}/>
        <div className="card-body">
            <h5 className="card-title"><a href={tileUrl}>{title}</a></h5>
            <p className="card-text">{description}</p>
        </div>
    </div>
);

Tile.propTypes = {
    tileUrl: PropTypes.string.isRequired,
    imageUrl: PropTypes.string.isRequired,
    title: PropTypes.string.isRequired,
    description: PropTypes.string
}

export default Tile