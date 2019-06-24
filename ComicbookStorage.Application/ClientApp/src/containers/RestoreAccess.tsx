import React, { Component } from "react";
import { bindActionCreators, Dispatch } from "redux";
import { connect } from 'react-redux';
import { History } from 'history';
import { InjectedFormProps, Field, reduxForm, WrappedFieldProps } from "redux-form";
import { Form, Button, Col, Row } from "reactstrap";
import { RestoreRequest } from "../store/restoreAccess/types";
import InputField, { InputProps } from "../components/InputField";
import { ApplicationState } from "../store/configureStore";
import { restoreAccessAsync } from "../store/restoreAccess/thunks";
import { required, email } from "../utils/validators";
import ErrorMessage from "../components/ErrorMessage";

type RestoreAccessProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps> &
    {
        history: History,
    };

export class RestoreAccess extends Component<InjectedFormProps<RestoreRequest, RestoreAccessProps> & RestoreAccessProps> {

    submit = (values: RestoreRequest) => {
        this.props.restore(values, this.props.history);
    }

    renderField = (fieldProperties: WrappedFieldProps & InputProps): JSX.Element => {
        return (<InputField {...fieldProperties} />);
    }

    render() {
        const { handleSubmit, submitting, restoreState } = this.props;

        return (
            <div>
                <div>
                    <h3>Restore access to your account</h3>
                    <p>Please enter your email and we will reset your password or resend the confirmation code if the email is not confirmed yet.</p>
                </div>
                <ErrorMessage errors={restoreState.errors} />
                <Form onSubmit={handleSubmit(this.submit)}>
                    <Field
                        name="email"
                        id="email"
                        type="email"
                        placeholder="user@example.com"
                        component={this.renderField}
                        label="Email*"
                        validate={[required, email]} />
                    <Row form>
                        <Col md={8}>
                            <Button type="submit" disabled={submitting} >Restore</Button>
                        </Col>
                    </Row>
                </Form >
            </div>
        );
    }
}

const mapStateToProps = (state: ApplicationState) => {
    return { restoreState: state.restoreAccess }
}

const mapDispatchToProps = (dispatch: Dispatch) => bindActionCreators(
    {
        restore: restoreAccessAsync,
    },
    dispatch
);

const restoreAccessForm = reduxForm<RestoreRequest, RestoreAccessProps>({
    form: "restoreAccess",
})(RestoreAccess);

export default connect(mapStateToProps, mapDispatchToProps)(restoreAccessForm)