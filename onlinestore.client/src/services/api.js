import axios from 'axios';

const API_URL = 'https://localhost:7009/api/';

export const getCustomers = async () => {
  const response = await axios.get(API_URL + 'customers');
  return response.data;
};

export const getCustomer = async (id) => {
  const response = await axios.get(API_URL + 'customers/' + id);
  return response.data;
};

export const addCustomer = async (customer) => {
  const response = await axios.post(API_URL + 'customers', customer);
  return response.data;
};

export const updateCustomer = async (customer) => {
  const response = await axios.put(API_URL + 'customers/' + customer.id, customer);
  return response.data;
};

export const deleteCustomer = async (id) => {
  const response = await axios.delete(API_URL + 'customers/' + id);
  return response.data;
};
