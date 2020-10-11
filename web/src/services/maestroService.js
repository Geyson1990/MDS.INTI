import http from './httpService';
import {hideData} from '../utils/security';

const endpoint = 'Maestro/';

const tokenObject = 'usuario';

function getLocalStorageItem(key) {
    return localStorage.getItem(key);
}

export function getJwt() {
    return JSON.parse(getLocalStorageItem(tokenObject)).token;
}

export const _maestroServiceListarPersonal = async (request) => {
    let param = { parametros: hideData(JSON.stringify(request)) };
    const response = await http.post(endpoint + 'listarPersonal', param);
    return response;
}

export default {
    _maestroServiceListarPersonal
};