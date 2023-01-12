import ldap from "ldapjs";
import { SvelteKitAuth } from "@auth/sveltekit";
import Credentials from '@auth/core/providers/credentials';

export const handle = SvelteKitAuth({
  providers: [
    Credentials({
      // The name to display on the sign in form (e.g. "Sign in with...")
      name: "Credentials",
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
      }
    }),
    Credentials({
      name: "LDAP",
      credentials: {
        username: { label: "DN", type: "text", placeholder: "" },
        password: { label: "Password", type: "password" },
      },
      async authorize(credentials, req) {
        // You might want to pull this call out so we're not making a new LDAP client on every login attemp
        const client = ldap.createClient({
          url: "ldaps://ldap1.uni-jena.de:636",
        })
        client.bind(credentials.username, credentials.password, (error) => {
          if (error) {
            console.error("Failed")
            return null
          } else {
            console.log("Logged in")
          }
        })

        const user = { id: "1", name: "J Smith", email: "jsmith@example.com" }
            return user
      }
    })
  ]
})
