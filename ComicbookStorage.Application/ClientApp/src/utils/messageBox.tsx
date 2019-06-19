import { NotificationManager } from 'react-notifications';

class MessageBox {

    showInfo(text: string) {
        NotificationManager.info(text, 'Info', 5000);
    }

    showError(text: string) {
        NotificationManager.error(text, 'Error message', 5000);
    }
}

const messageBox = new MessageBox();

export default messageBox