<template>
  <div class="login">
    <h2>Login</h2>
    <form @submit.prevent="handleLogin">
      <div class="form-group">
        <label for="username">Username</label>
        <input type="text" v-model="username" name="username" class="form-control" />
      </div>
      <div class="form-group">
        <label for="password">Password</label>
        <input type="password" v-model="password" name="password" class="form-control" />
      </div>
      <button type="submit" class="btn btn-primary btn-block">Login</button>
    </form>
  </div>
</template>

<script>
  import AuthService from '../services/auth';

  export default {
    name: 'LoginUser',
    data() {
      return {
        username: '',
        password: ''
      };
    },
    methods: {
      handleLogin() {
        AuthService.login({ username: this.username, password: this.password })
          .then(() => {
            this.$router.push('/');
          })
          .catch(error => {
            console.log(error);
          });
      }
    }
  };
</script>

<style scoped>
  .login {
    width: 300px;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }

  .form-group {
    margin-bottom: 15px;
  }

  .btn-block {
    width: 100%;
  }
</style>
