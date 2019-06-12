import React from 'react';
import { WrappedFieldProps } from "redux-form";
import { FormGroup, Label, FormText, Col, Input, FormFeedback, Spinner, Row, InputGroup, InputGroupAddon } from "reactstrap";

export interface InputProps {
    id: string,
    placeholder?: string,
    type: string,
    formText?: string,
}

const InputField = (props: WrappedFieldProps & InputProps) => {
    let displayError = props.meta.touched && typeof props.meta.error !== 'undefined';
    return (
        <Row form>
            <Col md={8}>
                <FormGroup>
                    <Label for={props.id}>{props.label}</Label>
                    <InputGroup className="flex-wrap">
                        <Input
                            {...props.input}
                            id={props.id}
                            type={props.type}
                            placeholder={props.placeholder}
                            invalid={displayError} />
                        {props.meta.asyncValidating &&
                            <InputGroupAddon addonType="append" className="d-inline-block align-middle mt-1 ml-n4">
                                <Spinner size="sm" type="grow" color="primary" />
                            </InputGroupAddon>}
                        {displayError && <FormFeedback className="w-100">{props.meta.error}</FormFeedback>}
                    </InputGroup>
                    {typeof props.formText !== "undefined" && <FormText color="muted">{props.formText}</FormText>}
                </FormGroup>
            </Col>
        </Row>
    );
}

export default InputField