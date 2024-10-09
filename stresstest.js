import { check } from 'k6';
import http from 'k6/http';

export const options = {
    vus: 10,
    duration: '10s',
    insecureSkipTLSVerify: true
};

export default () => {
    const url = 'http://localhost:5234/api/Regex/match';
    const payload = JSON.stringify(
        {
            "patternStored": "(\\+33|0) ?(\\d) ?(\\d\\d) ?(\\d\\d) ?(\\d\\d) ?(\\d\\d)",
            "pattern": "(\\+33|0) ?(\\d) ?(\\d\\d) ?(\\d\\d) ?(\\d\\d) ?(\\d\\d)",
            "text": "My phone is 06 12 23 45 56. Can you confirm that your phone is +33 6 11 22 33 44? I think that 0123456789 is the best phone number.",
            "options": 8,
            "substitutionPattern": "0$2-$3-$4-$5-$6"
        }
    );
    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };
    const res = http.post(url, payload, params);
    check(res, {
        'is status 400': (r) => r.status === 200,
    });
};