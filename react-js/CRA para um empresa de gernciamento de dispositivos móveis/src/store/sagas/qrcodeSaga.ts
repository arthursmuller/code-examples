import { PayloadAction } from "@reduxjs/toolkit";
import { takeLatest, call, put } from "redux-saga/effects";

import { QrCodeGenerateType } from "../../types/qrCode";
import { qrCodeError, qrCodeGenerateSuccess, Types } from "../qrcode";
import { api, safe } from "./util";



function* handleGenerate({payload: { fields }}: PayloadAction<{ fields: QrCodeGenerateType }>) {
    try {
        const returnData = yield call(api, 'qrcodeGenerate', fields)
        yield put(qrCodeGenerateSuccess(JSON.stringify(returnData)))
    }catch (e) {
        yield put(qrCodeError(e.body))
    }
}

export default function* qrcodeSaga() {
    yield takeLatest(Types.GENERATE, safe(handleGenerate));
}