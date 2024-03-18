export type ReportGpsType = {
  id: number,
  gps: boolean,
  createdAt: Date | string,
  device: {
    id: number,
    phoneNumber: number | string,
    deviceUser: {
      id: number,
      name: string
    }
  }
}
