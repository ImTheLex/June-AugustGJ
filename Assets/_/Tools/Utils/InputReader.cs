using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Shared
{
    [CreateAssetMenu(menuName = "Input/Input Reader")]
    public class InputReader : ScriptableObject, CustomPlayerActions.IPlayerActions,
        CustomPlayerActions.IPokemonActions, CustomPlayerActions.IUIActions, CustomPlayerActions.IMainMenuActions
    {

        private CustomPlayerActions _actions;

        // UI
        public Action PauseEvent;
        public Action ContinueEvent;
        public Action AnyKeyEvent;

        // Player
        public Action JumpEvent;

        //Pokemon
        public Action Attack1Event;
        public Action Attack2Event;
        public Action Attack3Event;
        public Action Attack4Event;

        //Else
        public Vector2 m_playerMove;
        public Vector2 m_cameraMove;
        public Vector2 m_pokemonMove;



        public void Initialize()
        {
            if (_actions != null) return;

            _actions = new CustomPlayerActions();
            _actions.Player.SetCallbacks(this);
            _actions.Pokemon.SetCallbacks(this);
            _actions.UI.SetCallbacks(this);
            _actions.MainMenu.SetCallbacks(this);
            //_actions.UI.Pause.performed += ctx => PauseEvent?.Invoke();
            _actions.Enable();
        }

        // ---Camera---
        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                m_cameraMove = context.ReadValue<Vector2>();
            }
        }
        // ---UI---
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                PauseEvent?.Invoke();
            }
        }

        public void OnContinue(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ContinueEvent?.Invoke();
            }
        }

        public void OnAnyKey(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                AnyKeyEvent?.Invoke();
            }
        }
        //---Player---
        public void OnPlayerMove(InputAction.CallbackContext context)
        {
            if (context.performed || context.started)
                m_playerMove = context.ReadValue<Vector2>();
            if (context.canceled)
                m_playerMove = Vector2.zero;
        }

        public void OnPokemonMove(InputAction.CallbackContext context)
        {
            if (context.performed || context.started)
                m_pokemonMove = context.ReadValue<Vector2>();
            if (context.canceled)
                m_pokemonMove = Vector2.zero;
        }

        //---Pokemon---
        public void OnRecall(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnAttack1(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Attack1Event?.Invoke();
            }
        }

        public void OnAttack2(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Attack2Event?.Invoke();
            }
        }

        public void OnAttack3(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Attack3Event?.Invoke();
            }
        }

        public void OnAttack4(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Attack4Event?.Invoke();
            }
        }

        public void OnPlayerJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpEvent?.Invoke(); // ou Invoke(true) si tu veux
            }
        }

        
        // --- Input Map Switching ---
        private void SwitchToMap(InputActionMap mapToEnable)
        {
            _actions.Disable(); // Désactive tout proprement
            _hasPressedAnyKey = false;
            mapToEnable.Enable();
        }

        public void EnableMenuMap()    => SwitchToMap(_actions.MainMenu);
        public void EnableUIMap()      => SwitchToMap(_actions.UI);
        public void EnablePlayerMap()  => SwitchToMap(_actions.Player);

        public void ResetInput()
        {
            _actions.Disable();
            _hasPressedAnyKey = false;
            m_playerMove = Vector2.zero;
            m_pokemonMove = Vector2.zero;
        }

        private bool _hasPressedAnyKey;
        
    }

}
