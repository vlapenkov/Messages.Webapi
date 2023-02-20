import { ToastMessageOptions } from 'primevue/toast';
import { ToastServiceMethods } from 'primevue/toastservice';

export function useToastNotificationHandler(toast: ToastServiceMethods) {
  const notifyHandler = (arg: ToastMessageOptions) => {
    toast.add(arg);
  };
  return notifyHandler;
}
