import React from 'react';
import { connect } from 'react-redux';

const CheckEmailConfirmationCode = () => (
    <div>
        <h3>Almost There... Check Your Email!</h3>
        <p>We have sent you a confirmation email. Before we can log you in we need to be sure the address you used was correctly entered.</p>
        <p>Check your inbox and click the link to confirm.</p>
    </div>
);

export default connect()(CheckEmailConfirmationCode);