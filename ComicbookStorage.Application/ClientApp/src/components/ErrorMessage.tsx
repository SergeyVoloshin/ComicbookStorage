import React from 'react';
import { Alert, Container, Row } from 'reactstrap';
import { ValidationErrors } from "../store/common/types";

export interface ErrorMessageProps {
    errors?: ValidationErrors
}

const ErrorMessage = ({ errors }: ErrorMessageProps) => {
    return (
        <div>
            {errors && <Alert color="danger">
                <Container>
                    {Object.values(errors)
                        .reduce((accumulator: string[], currentValue: string[]) => accumulator.concat(currentValue))
                        .map((error, index) => (<Row key={index}>{error}</Row>))}
                </Container>
            </Alert>}
        </div>);
}

export default ErrorMessage