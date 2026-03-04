// TypeScript types matching the .NET DTOs and models

export interface CarDTO {
  id: number;
  license_num: string;
  owner_name: string;
  entry_time: string;
  exit_time: string;
  total_payment: number;
}

export interface ParkingDTO {
  id: number;
  name: string;
  location: string;
  available_spots: number;
  price_per_hour: number;
}

export interface SpotDTO {
  id: number;
  spot_number: number;
  is_occupied: boolean;
  parking: ParkingDTO;
}

// Post/Put models
export interface CarPostModel {
  license_num: string;
  owner_name: string;
  parkingId: number;
}

export interface CarPutModel {
  owner_name: string;
}

export interface LoginModel {
  userName: string;
  password: string;
}

export interface ParkingPostModel {
  name: string;
  location: string;
  total_spots: number;
  price_per_hour: number;
}

export interface ParkingPutModel {
  id: number;
  name: string;
  price_per_hour: number;
  total_spots: number;
}

export interface SpotPostModel {
  spot_number: number;
  parkingId: number;
}

export interface SpotPutModel {
  id: number;
  is_occupied: boolean;
  carId: number;
}

export interface LoginResponse {
  token: string;
}

export interface CarDeleteResponse {
  message: string;
  paymentDue: string;
}
