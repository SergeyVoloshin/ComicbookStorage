import React from 'react';
import { Card, CardImg, CardText, CardBody, CardTitle } from 'reactstrap';

export interface TileProps {
    url: string,
    imageUrl: string,
    title: string,
    description: string,
}

const Tile = ({ url, imageUrl, title, description }: TileProps) => (
    <Card>
        <CardImg top src={imageUrl} alt={title} />
        <CardBody>
            <CardTitle><a href={url}>{title}</a></CardTitle>
            <CardText>{description}</CardText>
        </CardBody>
    </Card >
);

export default Tile