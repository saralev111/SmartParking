import {
  CarDTO, CarPostModel, CarPutModel, CarDeleteResponse,
  ParkingDTO, ParkingPostModel, ParkingPutModel,
  SpotDTO, SpotPostModel, SpotPutModel,
  LoginModel, LoginResponse,
} from "@/types/api";

// Change this to your .NET API base URL
const API_BASE = import.meta.env.VITE_API_BASE_URL || "https://localhost:7155";

function getHeaders(auth = false): HeadersInit {
  const headers: HeadersInit = { "Content-Type": "application/json" };
  if (auth) {
    const token = localStorage.getItem("sp_token");
    if (token) headers["Authorization"] = `Bearer ${token}`;
  }
  return headers;
}

async function handleResponse<T>(res: Response): Promise<T> {
  if (!res.ok) {
    if (res.status === 401) {
      localStorage.removeItem("sp_token");
      window.location.href = "/login";
    }
    const text = await res.text().catch(() => "");
    throw new Error(text || `Error ${res.status}`);
  }
  if (res.status === 204) return undefined as T;
  return res.json();
}

// Auth
export const authApi = {
  login: (data: LoginModel): Promise<LoginResponse> =>
    fetch(`${API_BASE}/api/Auth/login`, {
      method: "POST", headers: getHeaders(), body: JSON.stringify(data),
    }).then(r => handleResponse<LoginResponse>(r)),
};

// Parkings
export const parkingApi = {
  getAll: (): Promise<ParkingDTO[]> =>
    fetch(`${API_BASE}/api/Parkings`, { headers: getHeaders() })
      .then(r => handleResponse<ParkingDTO[]>(r)),

  getById: (id: number): Promise<ParkingDTO> =>
    fetch(`${API_BASE}/api/Parkings/${id}`, { headers: getHeaders() })
      .then(r => handleResponse<ParkingDTO>(r)),

  create: (data: ParkingPostModel): Promise<ParkingDTO> =>
    fetch(`${API_BASE}/api/Parkings`, {
      method: "POST", headers: getHeaders(true), body: JSON.stringify(data),
    }).then(r => handleResponse<ParkingDTO>(r)),

  update: (id: number, data: ParkingPutModel): Promise<ParkingDTO> =>
    fetch(`${API_BASE}/api/Parkings/${id}`, {
      method: "PUT", headers: getHeaders(), body: JSON.stringify(data),
    }).then(r => handleResponse<ParkingDTO>(r)),

  delete: (id: number): Promise<void> =>
    fetch(`${API_BASE}/api/Parkings/${id}`, {
      method: "DELETE", headers: getHeaders(true),
    }).then(r => handleResponse<void>(r)),
};

// Cars
export const carApi = {
  getAll: (): Promise<CarDTO[]> =>
    fetch(`${API_BASE}/api/Cars`, { headers: getHeaders(true) })
      .then(r => handleResponse<CarDTO[]>(r)),

  getById: (id: number): Promise<CarDTO> =>
    fetch(`${API_BASE}/api/Cars/${id}`, { headers: getHeaders(true) })
      .then(r => handleResponse<CarDTO>(r)),

  create: (data: CarPostModel): Promise<CarDTO> =>
    fetch(`${API_BASE}/api/Cars`, {
      method: "POST", headers: getHeaders(), body: JSON.stringify(data),
    }).then(r => handleResponse<CarDTO>(r)),

  update: (id: number, data: CarPutModel): Promise<CarDTO> =>
    fetch(`${API_BASE}/api/Cars/${id}`, {
      method: "PUT", headers: getHeaders(), body: JSON.stringify(data),
    }).then(r => handleResponse<CarDTO>(r)),

  delete: (id: number): Promise<CarDeleteResponse> =>
    fetch(`${API_BASE}/api/Cars/${id}`, {
      method: "DELETE", headers: getHeaders(true),
    }).then(r => handleResponse<CarDeleteResponse>(r)),
};

// Spots
export const spotApi = {
  getAll: (): Promise<SpotDTO[]> =>
    fetch(`${API_BASE}/api/Spots`, { headers: getHeaders() })
      .then(r => handleResponse<SpotDTO[]>(r)),

  getById: (id: number): Promise<SpotDTO> =>
    fetch(`${API_BASE}/api/Spots/${id}`, { headers: getHeaders() })
      .then(r => handleResponse<SpotDTO>(r)),

  create: (data: SpotPostModel): Promise<SpotDTO> =>
    fetch(`${API_BASE}/api/Spots`, {
      method: "POST", headers: getHeaders(true), body: JSON.stringify(data),
    }).then(r => handleResponse<SpotDTO>(r)),

  update: (id: number, data: SpotPutModel): Promise<SpotDTO> =>
    fetch(`${API_BASE}/api/Spots/${id}`, {
      method: "PUT", headers: getHeaders(), body: JSON.stringify(data),
    }).then(r => handleResponse<SpotDTO>(r)),

  delete: (id: number): Promise<void> =>
    fetch(`${API_BASE}/api/Spots/${id}`, {
      method: "DELETE", headers: getHeaders(true),
    }).then(r => handleResponse<void>(r)),
};
