import axios from "axios";
import { RequestConfig } from "./config";

export async function registerNewUserEndpoint(user){
    return await axios.post(`${RequestConfig.apiUrl}/api/Accounts/register`, user)
}

export async function loginUserEndpoint(user){
    return await axios.post(`${RequestConfig.apiUrl}/api/Accounts/Login`, user)
}

export async function createResourceEndpoint(res){
    return await axios.post(`${RequestConfig.apiUrl}/api/Resources/addResource`, res)
}

export async function getUserResourcesRoot(id){
    return await axios.get(`${RequestConfig.apiUrl}/api/Resources/${id}`)
}

export async function getUserResourceById(id){
    return await axios.get(`${RequestConfig.apiUrl}/api/Resources/resourceId/${id}`)
}

export async function getUserChecks(id){
    return await axios.get(`${RequestConfig.apiUrl}/api/PaymentChecks/${id}`)
}

export async function AddOwnershipCostEndpoint(cost){
    return await axios.post(`${RequestConfig.apiUrl}/api/Resources/addOwnershipCost`, cost)
}

export async function updateResourceEndpoint(resource){
    return await axios.put(`${RequestConfig.apiUrl}/api/Resources/updateUserResource`, resource)
}

export async function deleteResourceEndpoint(id){
    return await axios.delete(`${RequestConfig.apiUrl}/api/Resources/deleteUserResource/${id}`)
}