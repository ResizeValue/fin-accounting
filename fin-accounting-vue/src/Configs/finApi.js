import axios from "axios";
import { RequestConfig } from "./config";

//#region Auth

export async function loginUserEndpoint(user){
    return await axios.post(`${RequestConfig.apiUrl}/api/Accounts/Login`, user)
}

export async function registerNewUserEndpoint(user){
    return await axios.post(`${RequestConfig.apiUrl}/api/Accounts/register`, user)
}

//#endregion

//#region Resource

export async function getUserResourcesRoot(id){
    return await axios.get(`${RequestConfig.apiUrl}/api/Resources/user/${id}`)
}

export async function getUserResourceById(id){
    return await axios.get(`${RequestConfig.apiUrl}/api/Resources/${id}`)
}

export async function createResourceEndpoint(res){
    return await axios.post(`${RequestConfig.apiUrl}/api/Resources/addResource`, res)
}

export async function updateResourceEndpoint(resource){
    return await axios.put(`${RequestConfig.apiUrl}/api/Resources/updateUserResource`, resource)
}

export async function deleteResourceEndpoint(id){
    return await axios.delete(`${RequestConfig.apiUrl}/api/Resources/deleteUserResource/${id}`)
}

//#endregion

//#region Payment check

export async function getUserChecks(id){
    return await axios.get(`${RequestConfig.apiUrl}/api/PaymentChecks/user/${id}`)
}

export async function getChecksById(id){
    return await axios.get(`${RequestConfig.apiUrl}/api/PaymentChecks/${id}`)
}

export async function addPaymentCheck(check){
    return await axios.post(`${RequestConfig.apiUrl}/api/PaymentChecks/AddCheck`, check)
}

export async function updatePaymentCheck(check){
    return await axios.put(`${RequestConfig.apiUrl}/api/PaymentChecks/UpdateCheck`, check)
}

//#endregion

//#region Tag

export async function getUserTags(id){
    return await axios.get(`${RequestConfig.apiUrl}/api/Tags/user/${id}`)
}

//#endregion

//#region Ownership cost

export async function addOwnershipCostEndpoint(cost){
    return await axios.post(`${RequestConfig.apiUrl}/api/Resources/addOwnershipCost`, cost)
}

//#endregion
