import { setAuthorization } from 'common/client';
import { DecodedJwtData, parseJwt } from 'utils/parse-jwt';

type Request = (token: string) => void;

const TOKEN = '@plataformacliente/jwt';
class RequestUtils {
  pendingRequests: Request[] = [];

  storage: any;

  decodedJwt: DecodedJwtData | null;

  jwt: string | null;

  hasInterceptors = false;

  constructor() {
    this.storage = (typeof window !== 'undefined' &&
      window?.sessionStorage) || {
      getItem: (token: string) => this[token],
      setItem: (token: string, value: any) => {
        this[token] = value;
      },
    };

    const jwt = this.storage.getItem(TOKEN) || null;
    this.jwt = jwt;

    this.decodedJwt = jwt ? (parseJwt(jwt) as DecodedJwtData) : null;

    setAuthorization(jwt);
  }

  add(request: Request): void {
    this.pendingRequests.push(request);
  }

  getAll(): Request[] {
    return this.pendingRequests;
  }

  clear(): void {
    this.pendingRequests = [];
  }

  hasPendingRequest(): boolean {
    return this.pendingRequests.length > 0;
  }

  checkJwtExpired(): boolean {
    if (!this.decodedJwt) {
      return false;
    }

    return this.decodedJwt.exp < Date.now() / 1000;
  }

  setJwt(jwt: string): void {
    this.jwt = jwt;
    this.decodedJwt = parseJwt(jwt) as DecodedJwtData;
    this.storage.setItem(TOKEN, jwt);
    setAuthorization(jwt);
  }

  getJwt(): string | null {
    return this.jwt;
  }

  getDecodedJwt(): DecodedJwtData | null {
    return this.decodedJwt;
  }

  removeJwt(): void {
    this.jwt = null;
    this.decodedJwt = null;
    this.storage.removeItem(TOKEN);
    setAuthorization(null);
  }
}

export const RequestUtilsSingleton = new RequestUtils();
