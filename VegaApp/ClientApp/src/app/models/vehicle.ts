import { Contact } from './contact';
import { KeyValuePair } from './key-value-pair';

export interface Vehicle {
    id: number;
    model: KeyValuePair;
    make: KeyValuePair;
    isRegistered: boolean;
    features: KeyValuePair;
    contact: Contact;
    lastUpdate: string;
}