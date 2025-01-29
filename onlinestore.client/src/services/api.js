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

export const getCategories = async () => {
  const response = await axios.get(API_URL + 'categories');
  return response.data;
};

export const getCategory = async (id) => {
  const response = await axios.get(API_URL + 'categories/' + id);
  return response.data;
};

export const addCategory = async (category) => {
  const response = await axios.post(API_URL + 'categories', category);
  return response.data;
};

export const updateCategory = async (category) => {
  const response = await axios.put(API_URL + 'categories/' + category.id, category);
  return response.data;
};

export const deleteCategory = async (id) => {
  const response = await axios.delete(API_URL + 'categories/' + id);
  return response.data;
};

export const getItems = async () => {
  const response = await axios.get(API_URL + 'items');
  return response.data;
};

export const getItem = async (id) => {
  const response = await axios.get(API_URL + 'items/' + id);
  return response.data;
};

export const addItem = async (item) => {
  const response = await axios.post(API_URL + 'items', item);
  return response.data;
};

export const updateItem = async (item) => {
  const response = await axios.put(API_URL + 'items/' + item.id, item);
  return response.data;
};

export const deleteItem = async (id) => {
  const response = await axios.delete(API_URL + 'items/' + id);
  return response.data;
};

export const getOrders = async () => {
  const response = await axios.get(API_URL + 'orders');
  return response.data;
};

export const getOrder = async (id) => {
  const response = await axios.get(API_URL + 'orders/' + id);
  return response.data;
};

export const addOrder = async (order) => {
  const response = await axios.post(API_URL + 'orders', order);
  return response.data;
};

export const updateOrder = async (order) => {
  const response = await axios.put(API_URL + 'orders/' + order.id, order);
  return response.data;
};

export const deleteOrder = async (id) => {
  const response = await axios.delete(API_URL + 'orders/' + id);
  return response.data;
};

export const getOrderItems = async () => {
  const response = await axios.get(API_URL + 'orderitems');
  return response.data;
};

export const getOrderItem = async (id) => {
  const response = await axios.get(API_URL + 'orderitems/' + id);
  return response.data;
};

export const addOrderItem = async (orderItem) => {
  const response = await axios.post(API_URL + 'orderitems', orderItem);
  return response.data;
};

export const updateOrderItem = async (orderItem) => {
  const response = await axios.put(API_URL + 'orderitems/' + orderItem.id, orderItem);
  return response.data;
};

export const deleteOrderItem = async (id) => {
  const response = await axios.delete(API_URL + 'orderitems/' + id);
  return response.data;
};
export const getRoles = async () => {
  const response = await axios.get(API_URL + 'roles');
  return response.data;
};

export const getRole = async (id) => {
  const response = await axios.get(API_URL + 'roles/' + id);
  return response.data;
};

export const addRole = async (role) => {
  const response = await axios.post(API_URL + 'roles', role);
  return response.data;
};

export const updateRole = async (role) => {
  const response = await axios.put(API_URL + 'roles/' + role.id, role);
  return response.data;
};

export const deleteRole = async (id) => {
  const response = await axios.delete(API_URL + 'roles/' + id);
  return response.data;
};
export const getStatuses = async () => {
  const response = await axios.get(API_URL + 'statuses');
  return response.data;
};

export const getStatus = async (id) => {
  const response = await axios.get(API_URL + 'statuses/' + id);
  return response.data;
};

export const addStatus = async (status) => {
  const response = await axios.post(API_URL + 'statuses', status);
  return response.data;
};

export const updateStatus = async (status) => {
  const response = await axios.put(API_URL + 'statuses/' + status.id, status);
  return response.data;
};

export const deleteStatus = async (id) => {
  const response = await axios.delete(API_URL + 'statuses/' + id);
  return response.data;
};
export const getUsers = async () => {
  const response = await axios.get(API_URL + 'users');
  return response.data;
};

export const getUser = async (id) => {
  const response = await axios.get(API_URL + 'users/' + id);
  return response.data;
};

export const addUser = async (user) => {
  const response = await axios.post(API_URL + 'users', user);
  return response.data;
};

export const updateUser = async (user) => {
  const response = await axios.put(API_URL + 'users/' + user.id, user);
  return response.data;
};

export const deleteUser = async (id) => {
  const response = await axios.delete(API_URL + 'users/' + id);
  return response.data;
};
