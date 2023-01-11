import { SvelteKitAuth } from "@auth/sveltekit"
import Credentials from '@auth/core/providers/credentials';

export const handle = SvelteKitAuth({
  providers: [
    Credentials({
      // The name to display on the sign in form (e.g. "Sign in with...")
      name: "Credentials",
      type: "credentials",
      id: "credentials",
      // `credentials` is used to generate a form on the sign in page.
      // You can specify which fields should be submitted, by adding keys to the `credentials` object.
      // e.g. domain, username, password, 2FA token, etc.
      // You can pass any HTML attribute to the <input> tag through the object.
      credentials: {
        username: { label: "Username", type: "text", placeholder: "jsmith" },
        password: {  label: "Password", type: "password" }
      },
      async authorize(credentials, req) {
        //const authResponse = await fetch("https://taerar.infinite-trajectory.de/api/Login", {
        const authResponse = await fetch("https://taerar.infinite-trajectory.de/api/login", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(credentials)
        })

        if (!authResponse.ok) {
          return null
        }
        const user = await authResponse.json()
        return user
      },
    })
  ]
})
