import { NotificationManager } from 'react-notifications';

class MessageBox {
    showError(text: string) {
        NotificationManager.error(text, 'Error message', 5000);
    }

    showGeneralError() {
        this.showError('Something went completely wrong');
    }
}

const messageBox = new MessageBox();

export default messageBox